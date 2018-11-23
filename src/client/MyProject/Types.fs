module MyProject.Types
open Shared.Domain

type Model =
   { Dummy : Dummy.Types.Model list
     NewDummy : Dummy.Types.Model option
     Loading : bool }

type Msg =
   | ApiMsg of Api.Msg
   | DummyMsg of Dummy.Types.Msg



