// https://community.topcoder.com/stat?c=problem_statement&pm=12296

type TomekPhone() =
  member this.minKeystrokes(frequencies: int[], keySizes: int[]) : int =
    let presses =
      keySizes
      |> Array.collect (fun x -> Array.init x ((+) 1))
      |> Array.sort
    if frequencies.Length > presses.Length then
      -1
    else
      frequencies
      |> Array.sortDescending
      |> Array.mapi (fun i x -> x * presses.[i])
      |> Array.sum

// ----

(
  stdin.ReadLine().Split() |> Array.map int,
  stdin.ReadLine().Split() |> Array.map int
)
|> TomekPhone().minKeystrokes
|> stdout.WriteLine
