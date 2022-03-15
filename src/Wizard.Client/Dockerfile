#STAGE 1
FROM node:latest AS build
WORKDIR /src
COPY package.json package-lock.json ./
RUN npm ci
COPY . .
RUN npm install
RUN npm run ng build

#STAGE 2
FROM nginx:latest
COPY ./nginx.conf /etc/nginx/nginx.conf
RUN rm -rf /usr/share/nginx/html/*
COPY --from=build /src/dist/project /usr/share/nginx/html
