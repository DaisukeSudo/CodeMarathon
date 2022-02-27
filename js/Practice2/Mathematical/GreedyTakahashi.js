// https://atcoder.jp/contests/abc149/tasks/abc149_b

const main = ([a, b, k]) =>
  `${Math.max(0, a - k)} ${Math.max(0, b - Math.max(0, (k - a)))}`;

console.log(main(require('fs').readFileSync(0).toString().split(' ')));

// https://atcoder.jp/contests/abc149/submissions/29732926
