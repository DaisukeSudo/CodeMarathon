// ----- 基本 -----

Object.prototype.let = function(f) {
  return f(this.valueOf());
}

require('fs')
  .readFileSync('/dev/stdin', 'utf8')
  .let(x => x)
  .let(console.log);

// ----------

const collect = (list, fn) => (
  list && list.map(fn).reduce((acc, x) => acc.concat(x), [])
);

Array.prototype.collect = function(fn) {
  return collect(this, fn);
}

// [3,4,5].collect(x => [...new Array(x).keys()].map(x => x + 1))
// >> [1, 2, 3, 1, 2, 3, 4, 1, 2, 3, 4, 5]


const scan = (list, fn, initialValue) => (
  list && list.reduce(
    ([acc, scanAcc], cur, idx, src) => {
      const ret = fn(acc, cur, idx, src);
      scanAcc.push(ret);
      return [ret, scanAcc];
    },
    [initialValue, []]
  )[1]
);

Array.prototype.scan = function(fn, initialValue) {
  return scan(this, fn, initialValue);
}

// [1, 2, 3, 4, 5].scan((a, b) => a + b, 0)
// >> [1, 3, 6, 10, 15]
