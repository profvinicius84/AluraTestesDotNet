import http from 'k6/http';
import { check } from 'k6';

export const options = {
  vus: 10, // número de usuários virtuais simultâneos
  duration: '30s', // duração total do teste
};

export default function () {
  const res = http.get('http://localhost:5241/artistas');
  check(res, {
    'status is 200': (r) => r.status === 200,
  });
}