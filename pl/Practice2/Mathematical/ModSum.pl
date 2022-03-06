% https://atcoder.jp/contests/abc139/tasks/abc139_d

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

solve(N, Ret) :-
  Ret is N * (N - 1) / 2.

main :-
  read_number(N),
  solve(N, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc139/submissions/29920719
