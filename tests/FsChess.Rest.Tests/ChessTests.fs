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
            app.UseGiraffe <| Chess.webapi FsChess.App.Api.api)

let get (endpoint : string) =
    task {
        use server = new TestServer(getTestHost())
        use client = server.CreateClient()
        return! client.GetAsync(endpoint)
    }

[<Tests>]
let getNewGameTests =
    testList "GET request to /chess/games/new" [
        testCaseTask "succeeds to return a game" <| fun _ ->
            task {
                let! response = get "/chess/games/new"
                Expect.equal response.StatusCode HttpStatusCode.OK ""
            }
    ]


[<Tests>]
let getGameTests =
    testList "GET request to /chess/games/[moves]" [
        testCaseTask "succeeds to return a game" <|  fun _ ->
            task {
                let! response = get "/chess/games/e4/e5"
                Expect.equal response.StatusCode HttpStatusCode.OK ""
            }
    ]
