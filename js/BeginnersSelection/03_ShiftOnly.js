// https://atcoder.jp/contests/abs/tasks/abc081_b

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split('\n')[1]
  .split(' ')
  .map(x => (+x).toString(2))
  .map(x => x.length - x.lastIndexOf('1') - 1)
  .let(x => Math.min(...x))
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21478026
