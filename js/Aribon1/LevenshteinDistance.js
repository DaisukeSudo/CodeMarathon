// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_E&lang=jp

const solve = (s1, s2) => {
  const len1 = s1.length;
  const len2 = s2.length;

  // dp[i][j] = s1 の最初の i 文字を s2 の最初の j 文字に変換するための最小操作数
  const dp = Array(len1 + 1).fill(0).map(() => Array(len2 + 1).fill(0));

  // dp[i][0] = s1 の最初の i 文字を空文字列にするための削除操作数
  for (let i = 0; i <= len1; i++) dp[i][0] = i;
  // dp[0][j] = 空文字列を s2 の最初の j 文字にするための挿入操作数
  for (let j = 0; j <= len2; j++) dp[0][j] = j;

  for (let i = 1; i <= len1; i++) {
    for (let j = 1; j <= len2; j++) {
      // 文字が一致する場合
      if (s1[i - 1] === s2[j - 1]) {
        dp[i][j] = dp[i - 1][j - 1]; // 操作不要
      }
      // 文字が一致しない場合
      else {
        dp[i][j] = Math.min(
          dp[i - 1][j] + 1,     // 削除: s1 の i - 1 番目の文字を削除
          dp[i][j - 1] + 1,     // 挿入: s2 の j - 1 番目の文字を挿入
          dp[i - 1][j - 1] + 1  // 置換: s1 の i - 1 番目の文字と s2 の j - 1 番目の文字を置換
        );
      }
    }
  }

  return dp[len1][len2];
}

const main = input => (
  (([s1, s2]) => solve(s1, s2))(input.trim().split('\n'))
);

console.log(main(require('fs').readFileSync('/dev/stdin', 'utf8')));

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=9824001
