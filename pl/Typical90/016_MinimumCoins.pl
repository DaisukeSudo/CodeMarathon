% https://atcoder.jp/contests/typical90/tasks/typical90_p

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

search(N, A, B, C, X) :-
  MI is min(div(N, A), 9999),
  between(0, MI, I),
  N1 is N - A * I,
  MJ is min(div(N1, B), 9999 - I),
  between(0, MJ, J),
  N2 is N1 - B * J,
  N2 mod C =:= 0,
  X is I + J + N2 / C.

solve(N, A, B, C, Ret) :-
  findall(X, search(N, A, B, C, X), Xs),
  min_list(Xs, Ret).

main :-
  read_number(N),
  read_number(A),
  read_number(B),
  read_number(C),
  solve(N, A, B, C, Ret),
  write(Ret).

:- main.
