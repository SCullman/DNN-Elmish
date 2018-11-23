module MyProject.Api
 
open MyProject.Shared.Api
open Elmish
open Fable.Core.JsInterop
open Fable.PowerPack
open Fable.PowerPack.Fetch
open Thoth.Json
open MyProject.Shared.Domain

type Msg =
| Result of Event list
| Fetched of Result
| BatchFetched of Result array
| FetchFailed of exn

module Repository =
   open Fable.Import
   open Thoth.Json

   type Id = System.Guid

   let private storage = Browser.localStorage
   let inline private toJson x = Encode.Auto.toString(0, x)

   let inline private ofJson<'T> json = Decode.Auto.unsafeFromString<'T>(json)

   let inline private get<'t> key  =
      storage.getItem key  
      |> function | null -> None | x -> Some ( unbox x )
      |> Option.map ofJson<'t>

   let inline private set key value  = 
       storage.setItem ( key, toJson ( value ) )

   let inline updateItem name key value =
      get name
      |> function  | Some map -> map | None -> Map.empty
      |> Map.add key value 
      |> set name

   let inline getItems<'t> name =
      get<Map<Id,'t>> name 
      |> Option.map ( fun   m -> m |> Map.toList |> List.map snd ) 
      |> Option.defaultValue []

   let inline deleteItem name key value =
      get name
      |> function  | Some map -> map | None -> Map.empty
      |> Map.remove key
      |> set name
open Repository

let private handle' ( cmd : Command ) =
   promise {
     match cmd with
     | Command.Upsert dummy->
        updateItem "Dummies" dummy.Id dummy  
     return  [ Event.Handled cmd ] }
   
let private query' query = 
   promise {
     match query with 
     | Query.Dummy -> 
         return Result.Dummy ( getItems "Dummies" )
   }

let private batchQuery' qs = 
   promise {
      return! qs |> Seq.map ( query' ) |> Promise.Parallel
   }

open MyProject.Shared.Api
let handle cmd = Cmd.ofPromise handle' cmd Result FetchFailed
let query q    = Cmd.ofPromise query' q Fetched FetchFailed
let batchQuery qs = Cmd.ofPromise batchQuery' qs BatchFetched FetchFailed 
