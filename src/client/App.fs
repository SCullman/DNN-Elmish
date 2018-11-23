module App.View

open Elmish
open Fable.Core.JsInterop

importAll "../../sass/main.sass"

open Elmish.React
open Elmish.HMR

open MyProject
open MyProject.Global

Program.mkProgram State.init State.update View.render
|> Program.withConsoleTrace
|> Program.withReact fableModule.Platzhalter
|> Program.run
