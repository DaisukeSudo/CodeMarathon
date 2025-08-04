ThisBuild / scalaVersion := "2.13.16"

// Competitive programming setup
// Each file is treated as independent
lazy val competitiveProgramming = (project in file("."))
  .settings(
    name := "competitive-programming",
    // Don't treat warnings as errors
    scalacOptions ++= Seq(
      "-Xfatal-warnings:false",
      "-Ywarn-unused:false"
    ),
    // Add any dependencies you commonly use
    libraryDependencies ++= Seq(
      // Add your commonly used libraries here
    )
  )
