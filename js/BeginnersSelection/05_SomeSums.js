// https://atcoder.jp/contests/abs/tasks/abc083_b

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split(' ')
  .map(x => +x)
  .let(([n, a, b]) =>
    [...Array(n).keys()].map(x => x + 1)
      .filter(i =>
        i.toString()
          .split('')
          .map(x => +x)
          .reduce((a, x) => a + x)
          .let(x => a <= x && x <= b)
      )
  )
  .reduce((a, x) => a + x)
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21478051
