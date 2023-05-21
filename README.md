# qldt

docker build -t hufi-qldt .
docker run --detach --rm -p 80:80 -v /aspnet/https/:/https/:ro --name hufi-qldt hufi-qldt

docker run --name docker-nginx -p 443:443 -v ~/docker-nginx/html:/usr/share/nginx/html -v ~/docker-nginx/default.conf:/etc/nginx/conf.d/default.conf -d nginx
