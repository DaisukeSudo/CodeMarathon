% https://atcoder.jp/contests/abc150/tasks/abc150_d

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

reduce(P, [H|Ts], R) :-
  foldl(P, Ts, H, R).

unique([], []).
unique([H|Ts1], [H|Ts2]) :-
  delete(Ts1, H, Vs),
  unique(Vs, Ts2).

gcd(A, 0, A).
gcd(A, B, R) :- B > 0, M is A mod B, gcd(B, M, R).

lcm(A, B, R) :- gcd(A, B, G), R is A * B / G.

% ----

count(M, As, R) :-
  reduce(lcm, As, L),
  R is (div(M << 1, L) + 1) >> 1.

are_same_2s(As) :-
  convlist([X, Y] >> (Y is lsb(X)), As, Bs),
  unique(Bs, Cs),
  length(Cs, 1).

solve(M, As, R) :-
  are_same_2s(As) ->
    count(M, As, R);
    R is 0.

main :-
  read_number(N),
  read_number(M),
  read_numbers(N, As),
  solve(M, As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc150/submissions/29970891
