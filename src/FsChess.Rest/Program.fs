module FsChess.Rest.Program

#nowarn "20"

open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting

open Giraffe

open FsChess.App

[<EntryPoint>]
let main args =

    let builder = WebApplication.CreateBuilder(args)

    // builder.Services.AddControllers()
    builder.Services.AddGiraffe()

    let app = builder.Build()

    // app.UseHttpsRedirection()
    // app.UseAuthorization()
    // app.MapControllers()
    app.UseGiraffe <| Chess.webapi FsChess.App.Api.api

    app.Run()

    0
