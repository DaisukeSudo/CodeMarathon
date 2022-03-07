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

map(_P, [], []).
map(P, [H1|Ts1], [H2|Ts2]) :-
  call(P, H1, H2),
  map(P, Ts1, Ts2).

foldl(P, [H|Ts], A, R) :-
  call(P, A, H, A2),
  foldl(P, Ts, A2, R).
foldl(_P, [], R, R).

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
  R is (integer((M << 1) / L) + 1) >> 1.

bump(A, R) :-
  R is A /\ -A.

areSame2s(As) :-
  map(bump, As, Us),
  unique(Us, Vs),
  length(Vs, 1).

solve(M, As, R) :-
  areSame2s(As) ->
    count(M, As, R);
    R is 0.

main :-
  read_number(N),
  read_number(M),
  read_numbers(N, As),
  solve(M, As, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc150/submissions/29936877
