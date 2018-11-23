module MyProject.Api
 
open MyProject.Shared.Api
open Elmish
open Fable.Core.JsInterop
open Fable.PowerPack
open Fable.PowerPack.Fetch
open Thoth.Json

open MyProject.Global

type Msg =
| Result of Event list
| Fetched of Result
| BatchFetched of Result array
| FetchFailed of exn

let ( url, headers ) = DNN.moduleId fableModule.Platzhalter |> DNN.serviceFramework fableModule.ModuleName fableModule.ServiceName

let private resultDecoder = Decode.Auto.generateDecoder<Result>()
let private eventsDecoder = Decode.Auto.generateDecoder<Event list>()

let private request message= 
   [ RequestProperties.Method HttpMethod.POST
     Fetch.requestHeaders ( ContentType "application/json" :: headers )
     Credentials RequestCredentials.Sameorigin
     Body !^( Encode.Auto.toString (0, message) ) ] 
 
let private handle' ( cmd : Command ) = 
   Fetch.fetchAs ( url "dispatch" ) eventsDecoder ( request cmd )

let private query' ( query : Query ) =
   Fetch.fetchAs ( url "query" )  resultDecoder  ( request query )

let private batchQuery' qs = 
   qs |> Seq.map ( query' ) |> Promise.Parallel

let handle cmd = Cmd.ofPromise handle' cmd Result FetchFailed
let query q    = Cmd.ofPromise query' q Fetched FetchFailed
let batchQuery qs = Cmd.ofPromise batchQuery' qs BatchFetched FetchFailed 
