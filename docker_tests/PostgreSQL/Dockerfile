FROM postgres:alpine3.20

# ENV POSTGRES_USER=admin
# ENV POSTGRES_PASSWORD=admin
# ENV POSTGRES_DB=catalogDb

COPY "./init-user-db.sh" /docker-entrypoint-initdb.d

#RUN psql -U postgres -c 'SHOW config_file'
# Allows all connections from all the ips (host all all all scram-sha-256 -> host all all all trust) 

# \echo BETWEEN

# CMD [ "postgres" ]
# ENTRYPOINT [ "postgres" ]