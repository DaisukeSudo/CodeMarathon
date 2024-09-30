// https://codeforces.com/contest/462/problem/C

const solve = a => (
  ((total, last) => (
    a.reduce(([score, total], x) => [score + total, total - x], [total, total])[0] - last
  ))(
    a.reduce((a, x) => a + x),
    a.at(-1)
  )
);

const main = input => (
  solve(input.trim().split('\n')[1].split(' ').map(x => +x).sort())
);

// console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// ----

// How to submit a Node.JS solution on Code Forces
// https://codeforces.com/blog/entry/78878
process.stdin.resume();
process.stdin.setEncoding('utf-8');

let input = '';
process.stdin.on('data', x => input += x);
process.stdin.on('end',  _ => console.log(main(input)));

// RUNTIME ERROR
// https://codeforces.com/contest/462/submission/283830843
