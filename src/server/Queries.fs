module MyProject.Queries

open MyProject
open MyProject.Shared.Domain
open MyProject.Shared.Api
open MyProject.Mapper

let query context (query: Query) =

   match query with
   | Query.Dummy ->
      Repositories.Dummy.get context
      |> Seq.map Mapper.Dummy.toDomain
      |> Seq.toList
      |> Result.Dummy
