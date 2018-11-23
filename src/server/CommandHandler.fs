module MyProject.Handler

open MyProject.Shared.Api

let handle context cmd =
   
   match cmd with
   | Command.Upsert dummy -> 
      dummy
      |> Shared.Domain.Dummy.isValid 
      |> function
         | Ok dummy ->
            dummy |> Mapper.Dummy.fromDomain context |> Repositories.Dummy.upsert
         | Error message -> failwith message 
      
   [ Handled cmd ]