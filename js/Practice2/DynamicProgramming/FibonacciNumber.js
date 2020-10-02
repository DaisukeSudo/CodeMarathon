// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=ALDS1_10_A

const main = n => n < 0 ? 0 : n < 2 ? 1 :
  [...new Array(n - 1)].reduce(([p1, p2]) => [p1 + p2, p1], [1, 1])[0];

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// [...new Array(45).keys()].map(x => [x, main(x)]);
