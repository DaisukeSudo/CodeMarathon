// https://atcoder.jp/contests/abc006/tasks/abc006_4

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .trim()
  .split('\n')
  .map(x => +x)
  .let(arr => [
    arr[0],
    arr.slice(1),
    [...Array(arr[0] + 1)].map(_ => -1),
  ])
  .let(([n, ns, memo]) =>
    ns.reduce((len, x) =>
      memo[len] < x
        ? (_ => len + 1)(
          memo[len + 1] = x
        )
        : (_ => len)(
          memo.findIndex(x2 => x <= x2)
            .let(i => memo[i] = x)
        )
      , 0
    )
    .let(len => n - len)
  )
  .let(console.log);

// https://atcoder.jp/contests/abc006/submissions/21493390
