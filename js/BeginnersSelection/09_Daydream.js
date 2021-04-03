// https://atcoder.jp/contests/abs/tasks/arc065_a

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .trim()
  .let(s =>
    ['eraser', 'erase', 'dreamer', 'dream']
      .reduce(
        (arr, word) =>
          [].concat(...arr.map(x => x.split(word).filter(x => x))),
        [s]
      )
      .length == 0 ? 'YES' : 'NO'
  )
  .let(console.log);

// https://atcoder.jp/contests/abs/submissions/21477635
