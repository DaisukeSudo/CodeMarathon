// https://atcoder.jp/contests/tdpc/tasks/tdpc_iwi

let s = stdin.ReadLine()
let n = s.Length

let dp = Array.init (n + 1) (fun _ -> Array.create (n + 1) 0)

[3 .. n] |> List.iter (fun len ->
  [0 .. n - len] |> List.iter (fun l ->
    let r = l + len - 1
    [l .. r] |> List.iter (fun k ->
      dp.[l].[r] <- max dp.[l].[r] (dp.[l].[k] + dp.[k + 1].[r])
    )
    [l .. r] |> List.iter (fun m ->
      if s.[l] = 'i' && s.[m] = 'w' && s.[r] = 'i' then
        let lmv = if m - l - 1 >= 3 then dp.[l + 1].[m - 1] else 0
        let mrv = if r - m - 1 >= 3 then dp.[m + 1].[r - 1] else 0
        let elmv = (m - l - 1)
        let emrv = (r - m - 1)
        let v = if lmv * 3 + mrv * 3 = elmv + emrv then 1 else 0
        dp.[l].[r] <- max dp.[l].[r] (lmv + mrv + v)
    )
  )
)

dp.[0].[n - 1] |> stdout.WriteLine

// https://atcoder.jp/contests/tdpc/submissions/68060778
