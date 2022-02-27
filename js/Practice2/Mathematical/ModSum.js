// https://atcoder.jp/contests/abc139/tasks/abc139_d

const main = n =>
  n * (n - 1n) / 2n;

console.log(main(BigInt(require('fs').readFileSync(0).toString())).toString());

// https://atcoder.jp/contests/abc139/submissions/29736645
