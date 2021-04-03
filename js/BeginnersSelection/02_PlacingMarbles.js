// https://atcoder.jp/contests/abs/tasks/abc081_a

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split('')
  .map(x => +x)
  .reduce((a, x) => a + x)
  .let(console.log);

  // https://atcoder.jp/contests/abs/submissions/21478010
  