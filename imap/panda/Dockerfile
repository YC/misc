FROM debian:12

ENV DEBIAN_FRONTEND noninteractive

RUN apt update \
        && apt-get -y install --no-install-recommends git netcat-openbsd ca-certificates make clang socat dos2unix \
        && apt-get clean

WORKDIR /panda
RUN git clone https://github.com/jonabbey/panda-imap.git \
        && cd panda-imap \
        && echo y | make -j 12 slx SSLTYPE=none

COPY insert.sh /panda/
RUN /panda/insert.sh

CMD ["socat", "TCP4-LISTEN:143,reuseaddr,fork", "SYSTEM:'/panda/panda-imap/imapd/imapd'"]
