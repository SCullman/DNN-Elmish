module MyProject.Shared.Domain
open System

type DummyId = DummyId of Guid

module DummyId =
  let value (DummyId id) = id

type Dummy = 
  { Id : DummyId 
    Name : string option }

module Dummy =

   let empty () = 
      { Id = DummyId ( Guid.NewGuid () ) 
        Name = None }

   let create id = 
      { Id = DummyId id 
        Name = None }
   
   let name name dummy =
      { dummy with Name = if String.IsNullOrWhiteSpace name 
                             then None
                             else Some name } 
      
   let isValid dummy =
      match dummy.Name with
      | Some name when name |> String.length >= 2 -> Result.Ok dummy
      | _                                         -> Result.Error "Name should have at least 2 chars"