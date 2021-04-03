// https://atcoder.jp/contests/abs/tasks/arc089_a

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .trim()
  .split('\n')
  .slice(1)
  .map(x => x.split(' ').map(x2 => +x2))
  .reduce((prev, next) =>
    prev
      && [prev, next].let(([[t1, x1, y1], [t2, x2, y2]]) => (
        (t2 & 1) === ((x2 + y2) & 1)
          && ((t2 - t1) >= (Math.abs(x2 - x1) + Math.abs(y2 - y1)))
      ))
      && next,
    [0, 0, 0]
  )
  .let(x => x ? 'Yes' : 'No')
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21477949
