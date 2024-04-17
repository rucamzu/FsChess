namespace FsChess.Build

open Nuke.Common
open Nuke.Common.IO

/// Paths to directories and files of interest
module Paths =
    /// Path to the root directory
    let root = NukeBuild.RootDirectory
    /// Path to the tests directory
    let tests = root / "tests"
    /// Paths to all the test projects
    let testsProjects : AbsolutePath seq = tests.GlobFiles("**/*.Tests.fsproj")
