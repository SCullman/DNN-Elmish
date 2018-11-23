module MyProject.Shared.Api

open Domain

type Command =
   | Upsert of Dummy

type Event =
   | Handled of Command

type Query =
   | Dummy

type Result =
   | Dummy of Dummy list