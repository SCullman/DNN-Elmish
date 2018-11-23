module MyProject.Mapper

open MyProject.Repositories
open MyProject.Shared
open MyProject.Shared.Domain

open System

[<RequireQualifiedAccess>]
module Dummy =
   open Dummy

   let fromDomain context ( d: Domain.Dummy ) =
      { Id = d.Id |> DummyId.value
        Context = context
        Name = d.Name |> Option.defaultValue "" }

   let toDomain ( d: DummyDb ) : Domain.Dummy =
      Dummy.create d.Id
      |> Dummy.name d.Name
