// https://atcoder.jp/contests/abc134/tasks/abc134_e

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .trim()
  .split('\n')
  .map(x => +x)
  .let(arr => [
    arr.slice(1),
    [...Array(arr[0] + 1)].map(_ => 10 ** 9 + 1),
  ])
  .let(([ns, memo]) =>
    ns.reduce((len, x) =>
      memo[len] >= x ? (
        [memo[len + 1] = x]
          .let(_ => len + 1)
      )
      : (
        [...Array(17)]
          .reduce(([li, ri]) =>
            (li + Math.floor((ri - li) / 2)).let((mi) =>
              memo[mi] < x ? [li, mi] : [mi + 1, ri]
            )
            , [1, len]
          )
          .let(([i, _]) => [memo[i] = x])
          .let(_ => len)
      )
      , 0
    )
  )
  .let(console.log);

// https://atcoder.jp/contests/abc134/submissions/21551943
