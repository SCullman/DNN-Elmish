module MyProject.Repositories

open System
open DotNetNuke.Data
open DotNetNuke.ComponentModel.DataAnnotations


module Dummy  =
    
   let connectionStringName = "MyConnectionString"

   [<CLIMutable>]
   [<TableName "myproject_dummy">]
   [<Scope "Context">]
   [<PrimaryKey ("Id", AutoIncrement = false) >]
   type DummyDb = 
       { Id : Guid
         Context : string
         Name : string }  

   let withRepository callback =
      // ctx is IDiposable, therefore it can't be returned in a function, 
      // neither the repository
      // https://fsharpforfunandprofit.com/posts/let-use-do/
      
      use ctx = DataContext.Instance connectionStringName 
      ctx.GetRepository<DummyDb>() |> callback

   let get context =
      withRepository ( fun rep -> rep.Get context )

   let upsert dummy =
      withRepository ( fun rep ->
         if ( rep.Find ( "WHERE Id = @0", [ dummy.Id ] ) |> Seq.isEmpty )
         then rep.Insert dummy
         else rep.Update dummy )

   let delete dummy =
      withRepository ( fun rep -> rep.Delete dummy )
