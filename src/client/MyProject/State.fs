module MyProject.State
open Elmish
open MyProject
open MyProject.Shared.Domain
open MyProject.Shared.Api
open MyProject.Api
open Types

let init ()  =
   { Loading = true
     NewDummy = None
     Dummy = [] } , Cmd.map ApiMsg <| Api.query  Query.Dummy

let update msg model =
   match msg with
   | ApiMsg msg ->
      match msg with
      | Api.Fetched ( Result.Dummy dummies ) ->
         let (dummies, cmds) =
            dummies
            |> List.map( Dummy.State.init )
            |> List.unzip  
         { model with Dummy = dummies }, Cmd.batch cmds       
           
      | _ -> failwith "Not implemented" 

   | DummyMsg msg -> 
      match model.NewDummy with
      | Some dummy -> 
         let dummy, cmd = Dummy.State.update msg dummy
         { model with NewDummy = Some dummy }, Cmd.map DummyMsg cmd
      | _ -> model, Cmd.none

