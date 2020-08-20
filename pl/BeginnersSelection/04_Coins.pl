% https://atcoder.jp/contests/abs/tasks/abc087_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

goal(A, B, C, X) :-
  between(0, A, I),
  between(0, B, J),
  between(0, C, K),
  500 * I + 100 * J + 50 * K =:= X.

solve(A, B, C, X, Ret) :-
  findall(_, goal(A, B, C, X), List),
  length(List, Ret).

main :-
  read_number(A),
  read_number(B),
  read_number(C),
  read_number(X),
  solve(A, B, C, X, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16066001
