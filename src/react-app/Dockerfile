FROM node:12-slim AS build-env
WORKDIR /app

# install app dependencies
COPY package.json package-lock.json ./
RUN npm install

# copy everything else and build
COPY . .
RUN npm run build

# build runtime image
FROM nginx:mainline

COPY --from=build-env /app/dist /var/www
COPY ./nginx.conf /etc/nginx/conf.d/default.conf