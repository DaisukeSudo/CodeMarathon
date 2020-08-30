// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1160

// （入力を個別の問題ごとに分割）

// 行列を順に探索

// 海の場合はスキップ
// 陸の場合は続行

// 隣接セルを探索
// 四方向 > 左上, 上, 右上, 左

//   登録済みのノードのルートを取得
//   重複除去して1つ目をポップ
//   取れなかった場合
//     自身をルートとして登録
//   取れた場合
//     該当ルートを登録
//     ポップした残りのルートがある場合
//       残りのルートを持つノードのルートを全て上書き（島結合）

// ルートの数（島の数）を数える

const main = input => 
  input.trim().split('\n').slice(0, -1)
    .reduce(
      ({ ps, h }, line) => (
        !h
          ? (ps.push([]) && { ps, h: line.split(' ')[1] })
          : (ps[ps.length - 1].push(line.split(' ').map(x => Number(x))) && { ps, h: h - 1 })
      ),
      { ps: [], h: 0 }
    )
    .ps
    .map(island =>
      (
        nodes => [...new Set(nodes.map(([_, rk]) => rk))].length
      )(
        island.reduce((nodes, row, ri) =>
          row.map((cell, ci) =>
            !!cell && (
              (
                rks =>
                  (
                    ([rk, dks]) =>
                      !rk
                        ? nodes.push([`${ri}-${ci}`, `${ri}-${ci}`])
                        : nodes.push([`${ri}-${ci}`, rk]) && (
                          !!dks.length && (nodes = dks.reduce(
                              (nodes2, dk) =>
                                nodes2.map(([k, v]) => [k, v === dk ? rk : v]),
                              nodes
                          ))
                        )
                  )(
                    [rks.shift(), rks]
                  )
              )(
                [...new Set(
                  nodes
                    .filter(([k1]) =>
                      [
                        `${ri - 1}-${ci - 1}`,
                        `${ri - 1}-${ci    }`,
                        `${ri - 1}-${ci + 1}`,
                        `${ri    }-${ci - 1}`,
                      ].some(k2 => k1 === k2)
                    )
                    .map(([_, rk]) => rk)
                )]
              )
            )
          ) && nodes,
          []
        )
      )
    )
    .join('\n');

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));
