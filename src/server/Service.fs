namespace MyProject.Service

open DotNetNuke.Web.Api
open System.Web.Http
open System.Net
open System.Net.Http
open DotNetNuke.Entities.Portals
open Newtonsoft.Json
open System.Net.Http.Formatting
open System.Web.Http.Controllers
open Thoth.Json.Net.Converters

open MyProject

type WithThothJsonNetConverterAttribute () =
   inherit System.Attribute()

   interface IControllerConfiguration with
      member __.Initialize ( ( controllerSettings: HttpControllerSettings ) , _ ) =
        
        let converter = CacheConverter(converters)

        let thothFormatter = 
           JsonMediaTypeFormatter ( SerializerSettings = JsonSerializerSettings ( Converters = [|converter|] ) )
        controllerSettings.Formatters.Clear ()
        controllerSettings.Formatters.Add thothFormatter

[<WithThothJsonNetConverter>]
type FableController () =
  inherit DnnApiController ()

type RouteMapper ()=
  interface IServiceRouteMapper with
    member __.RegisterRoutes (rtm:IMapRoute) = 
      let namespaces = [|"MyProject.Service"|]
      rtm.MapHttpRoute  ("MyProject", "default", "{controller}/{action}", namespaces) |>ignore

type ServiceController  () as controller =
  inherit FableController ()

  let context = 
      let portalId = controller.PortalSettings.PortalId
      PortalController.GetPortalSetting ( "Context", portalId, "AContext" )

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  [<DnnModuleAuthorize ( AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View ) >]
  member __.Query query = 
    let result = Queries.query context query         
    __.Request.CreateResponse ( HttpStatusCode.OK, result )

  [<HttpPost>]
  [<ValidateAntiForgeryToken>]
  [<DnnModuleAuthorize ( AccessLevel = DotNetNuke.Security.SecurityAccessLevel.Edit ) >]
  member __.Handle cmd = 
    let events = Handler.handle context cmd
    __.Request.CreateResponse ( HttpStatusCode.OK, events )
