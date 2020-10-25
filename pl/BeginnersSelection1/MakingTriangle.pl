% https://atcoder.jp/contests/abc175/tasks/abc175_b

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

triangle(A, B, C) :-
  A =\= B,
  A =\= C,
  B =\= C,
  A + B > C,
  A + C > B,
  B + C > A.

satisfy(N, Ls) :-
  between(1, N, I),
  between(1, N, J),
  between(1, N, K),
  I < J,
  J < K,
  nth1(I, Ls, LI),
  nth1(J, Ls, LJ),
  nth1(K, Ls, LK),
  triangle(LI, LJ, LK).

solve(N, Ls, Ret) :-
  findall(_, satisfy(N, Ls), List),
  length(List, Ret).

main :-
  read_number(N),
  read_numbers(N, Ls),
  solve(N, Ls, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc175/submissions/17242801
