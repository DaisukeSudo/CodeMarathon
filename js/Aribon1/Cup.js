// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=0503

const solve = (n, m, initialA, initialB, initialC) => {
  const goal = [...Array(n).keys()].map(x => x + 1).toString();
  const initialState = [initialA, initialB, initialC, 0];
  const queue = [initialState];
  const visited = new Set();
  const visit = (state) => {
    const key = state.slice(0, 3).map(x => x.toString()).join('|');
    const notVisited = !visited.has(key);
    if (notVisited) visited.add(key);
    return notVisited;
  };
  visit(initialState);

  while (queue.length > 0) {
    let state = [curA, curB, curC, step] = queue.shift();
    if (curA.toString() === goal || curC.toString() === goal) return step;
    [
      [0, 1], // A -> B
      [1, 0], // B -> A
      [1, 2], // B -> C
      [2, 1], // C -> B
    ].forEach(([srcI, dstI]) => {
      const src = state[srcI];
      const dst = state[dstI];
      if (src.length === 0 || (dst.length > 0 && src[src.length - 1] < dst[dst.length - 1])) return;

      let src2 = src.slice(0, src.length - 1);
      let dst2 = dst.concat(src[src.length - 1]);
      let state2 = [...state];
      state2[srcI] = src2;
      state2[dstI] = dst2;
      state2[3] = step + 1;
      if (visit(state2) && step + 1 <= m) {
        queue.push(state2);
      }
    });
  }
  return -1;
}

const chunk = (arr, size) => {
  let result = [];
  for (let i = 0; i < arr.length; i += size) {
      result.push(arr.slice(i, i + size));
  }
  return result;
}

const main = input =>
  chunk(input.trim().split('\n').slice(0, -1), 4)
    .map(xs => xs.map(x => x.split(' ').map(x => +x)))
    .map(([[n, m], a, b, c]) => solve(n, m, a.slice(1), b.slice(1), c.slice(1)))
    .join('\n');

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=9112754
