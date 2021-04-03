// https://atcoder.jp/contests/abs/tasks/abc086_a

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split(' ')
  .map(x => x & 1)
  .reduce((a, x) => a & x)
  .let(x => x === 1 ? 'Odd' : 'Even')
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21477995
