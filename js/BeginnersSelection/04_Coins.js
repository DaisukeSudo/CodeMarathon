// https://atcoder.jp/contests/abs/tasks/abc087_b

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split('\n')
  .map(x => +x)
  .let(([a, b, c, x]) =>
    [...Array(Math.min(a, Math.floor(x / 500)) + 1).keys()]
      .map(i =>
        [...Array(Math.min(b, Math.floor((x - 500 * i) / 100)) + 1).keys()]
          .filter(j => x - 500 * i - 100 * j <= 50 * c)
          .length
      )
  )
  .reduce((a, x) => a + x)
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21478036
