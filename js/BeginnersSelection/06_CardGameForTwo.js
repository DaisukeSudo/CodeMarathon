// https://atcoder.jp/contests/abs/tasks/abc088_b

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split('\n')[1]
  .split(' ')
  .map(x => +x)
  .sort((a, b) => b - a)
  .reduce(([a, b], x, i) =>
    i % 2 == 0
      ? [a + x, b]
      : [a, b + x]
    , [0, 0]
  )
  .let(([a, b]) => a - b)
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21475903
