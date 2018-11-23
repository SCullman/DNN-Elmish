module MyProject.Dummy
open Shared.Domain

module Types =
   type Model = { Dummy: Dummy }
   type Msg = 
     | NameChanged of string 

module State = 
   open Types
   open Elmish

   let neu () = 
      { Dummy = Dummy.empty () }, Cmd.none

   let init d = 
      { Dummy = d }, Cmd.none

   let update msg model = 
      match msg with
      | NameChanged name ->
        { Dummy = model.Dummy |> Dummy.name name }, Cmd.none

module View =
   open Types
   open Fable.Helpers.React
   open Fable.Helpers.React.Props
   open Fable.Core.JsInterop

   let render model dispatch = 
      div []
         [ str "Name "
           input 
              [ OnChange (fun event -> event.target?value |> NameChanged |> dispatch)
                Placeholder "At least 2 chars"
                DefaultValue model.Name ] ]