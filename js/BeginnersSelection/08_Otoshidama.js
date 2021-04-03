// https://atcoder.jp/contests/abs/tasks/abc085_c

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .split(' ')
  .map(x => +x)
  .let(([n, y]) => [n, y / 1000])
  .let(([n, y]) =>
    ((v, f) => v != null ? f(v) : v).let(oc => oc(
      [...Array(n + 1).keys()].find(a =>
        (y - n - 9 * a)
          .let(x => x % 4 == 0 && x >= 0 && n - a - x / 4 >= 0)
      ),
      a => ((y - n - 9 * a) / 4)
        .let(b => `${a} ${b} ${n - a - b}`)
    ))
    || '-1 -1 -1'
  )
  .let(console.log);

// Optional chaining is available in Node.js ver 14.

// https://atcoder.jp/contests/abs/submissions/21477540
