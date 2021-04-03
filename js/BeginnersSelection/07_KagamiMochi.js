// https://atcoder.jp/contests/abs/tasks/abc085_b

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .trim()
  .split('\n')
  .slice(1)
  .let(x => [...new Set(x)])
  .length
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21476014
