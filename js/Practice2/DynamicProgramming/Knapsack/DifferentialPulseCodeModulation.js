// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=2199

const solve = (codebook, signals) => {
  const MIN_VAL = 0;
  const MAX_VAL = 255;
  const INF = Number.MAX_SAFE_INTEGER;

  const clamp = x => Math.max(MIN_VAL, Math.min(MAX_VAL, x))
  const createDp = () => new Array(MAX_VAL + 1).fill(INF);

  const initialDp = createDp();
  initialDp[128] = 0;

  const finalDp = signals.reduce((currentDp, target) => (
    currentDp
      .map((cost, v) => ({ cost, v }))
      .filter(({ cost }) => cost !== INF)
      .reduce((nextDp1, { cost, v }) => (
        codebook.reduce((nextDp2, code) => {
          const nextV = clamp(v + code);
          const diff = target - nextV;
          const newCost = cost + diff * diff;

          if (newCost < nextDp2[nextV]) {
            nextDp2[nextV] = newCost;
          }
          return nextDp2
        }, nextDp1)
      ), createDp())
  ), initialDp);

  return finalDp.reduce((acc, cost) => Math.min(acc, cost), INF);
};

const main = input => {
  const tokens = input.trim().split(/\s+/).map(Number);
  let index = 0;
  const results = [];

  while (index < tokens.length) {
    const n = tokens[index++];
    const m = tokens[index++];

    if (n === 0 && m === 0) break;

    const codebook = Array.from({ length: m }, () => tokens[index++]);
    const signals  = Array.from({ length: n }, () => tokens[index++]);

    results.push(solve(codebook, signals));
  }

  return results.join('\n');
};

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=11760163
// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=11760190
