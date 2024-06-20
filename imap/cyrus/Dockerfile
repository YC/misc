FROM debian:12

ENV DEBIAN_FRONTEND noninteractive

RUN apt update \
        && apt-get -y install --no-install-recommends cyrus-imapd sasl2-bin dos2unix netcat-openbsd \
        && apt-get clean

RUN mkdir -p /run/cyrus/proc \
        && chown -R cyrus /run/cyrus

ADD cyrus.conf imapd.conf /etc/

RUN echo "test" | saslpasswd2 -u test -c test -p

WORKDIR /cyrus
COPY insert.sh /cyrus
RUN /cyrus/insert.sh

CMD /usr/sbin/cyrmaster
