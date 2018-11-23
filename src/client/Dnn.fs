[<RequireQualifiedAccess>]
module DNN

open Fable.Core
open Fable.PowerPack.Fetch.Fetch_types
open Fable.Import.Browser

let moduleId placeholderId =  
  ( document.getElementById placeholderId ).dataset.["moduleid"]
  |> TryParser.parseInt

type IServicesFramework = 
  abstract getServiceRoot: string     -> string 
  abstract setModuleHeaders: obj      -> unit
  abstract getTabId : unit            -> int option
  abstract getModuleId : unit         -> int option 
  abstract getAntiForgeryValue : unit -> string option

[<Emit("window['$'].ServicesFramework($0)")>]
let private ServiceFramework moduleid : IServicesFramework  = jsNative 

let private moduleHeaders ( sf:IServicesFramework )  = 
   [ HttpRequestHeaders.Custom ( "ModuleId", sf.getModuleId () )
     HttpRequestHeaders.Custom ( "TabId", sf.getTabId () )
     HttpRequestHeaders.Custom ( "RequestVerificationToken", sf.getAntiForgeryValue () ) ]

let serviceFramework moduleName endpoint moduleId  = 
   let sf = ServiceFramework moduleId    
   let url = sprintf "%s%s/%s" ( sf.getServiceRoot moduleName )  endpoint 
   url, ( moduleHeaders sf )
