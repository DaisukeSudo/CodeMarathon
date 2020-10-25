% https://atcoder.jp/contests/abc133/tasks/abc133_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

read_numbers(0, []).
read_numbers(N, [X|Xs]) :-
  read_number(X),
  N1 is N - 1,
  read_numbers(N1, Xs).

read_numbers_list(0, _, []).
read_numbers_list(H, W, [Xs|Xss]) :-
  read_numbers(W, Xs),
  H1 is H - 1,
  read_numbers_list(H1, W, Xss).

df_sq_list([], _, []) :- !.
df_sq_list(_, [], []) :- !.
df_sq_list([X1|X1s], [X2|X2s], [Y|Ys]) :-
  X3 is X1 - X2,
  Y is X3 * X3,
  df_sq_list(X1s, X2s, Ys).

satisfy(N, Xss) :-
  between(1, N, I),
  between(1, N, J),
  I < J,
  nth1(I, Xss, Is),
  nth1(J, Xss, Js),
  df_sq_list(Is, Js, Ks),
  sum_list(Ks, Sum),
  Dist1 is sqrt(Sum),
  Dist2 is floor(Dist1),
  Dist1 =:= Dist2.

solve(N, D, Xss, Ret) :-
  findall(_, satisfy(N, Xss), List),
  length(List, Ret).

main :-
  read_number(N),
  read_number(D),
  read_numbers_list(N, D, Xss),
  solve(N, D, Xss, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc133/submissions/17241666
