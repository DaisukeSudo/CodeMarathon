% https://atcoder.jp/contests/sumitrust2019/tasks/sumitb2019_e

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

step(X, [X, X, X], [A, X, X], R1, R2) :- A is X + 1, R2 is R1 * 3 mod 1000000007.
step(X, [X, X, C], [A, X, C], R1, R2) :- A is X + 1, R2 is R1 * 2 mod 1000000007.
step(X, [A, X, X], [A, B, X], R1, R2) :- B is X + 1, R2 is R1 * 2 mod 1000000007.
step(X, [X, B, C], [A, B, C], R1, R2) :- A is X + 1, R2 is R1 * 1 mod 1000000007.
step(X, [A, X, C], [A, B, C], R1, R2) :- B is X + 1, R2 is R1 * 1 mod 1000000007.
step(X, [A, B, X], [A, B, C], R1, R2) :- C is X + 1, R2 is R1 * 1 mod 1000000007.
step(X, P, N, R1, 0).

loop([], _, Ret, Ret).
loop([H|Ts], P, R1, Ret) :-
  step(H, P, N, R1, R2),
  loop(Ts, N, R2, Ret).

solve([], 0).
solve(As, Ret) :-
  loop(As, [0, 0, 0], 1, Ret).

main :-
  read_number(N),
  read_numbers(N, As),
  solve(As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/sumitrust2019/submissions/30219951
