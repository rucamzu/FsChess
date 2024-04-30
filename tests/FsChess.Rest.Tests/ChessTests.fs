module FsChess.Rest.Tests.ChessTests

open Microsoft.AspNetCore.TestHost
open Microsoft.AspNetCore.Hosting
open System.Net

open Giraffe
open Expecto
open FsChess.App
open FsChess.Rest

let getTestHost () =
    WebHostBuilder()
        .UseTestServer()
        .ConfigureServices(fun services ->
            services.AddGiraffe() |> ignore)
        .Configure(fun app ->
            app.UseGiraffe <| Chess.webapi Chess.Api.api)

let get (endpoint : string) =
    task {
        use server = new TestServer(getTestHost())
        use client = server.CreateClient()
        return! client.GetAsync(endpoint)
    }

[<Tests>]
let newGameTests =
    testList "/chess/games/new" [
        testCaseTask "returns initial game" <| fun _ ->
            task {
                let! response = get "/chess/games/new"
                Expect.equal response.StatusCode HttpStatusCode.OK ""
            }
    ]
