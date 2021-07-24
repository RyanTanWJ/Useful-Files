package com.ryan.user.service.ryanuserservice.datastore.repository;

import com.ryan.user.service.ryanuserservice.datastore.document.User;
import org.springframework.data.mongodb.repository.MongoRepository;

public interface UserRepository extends MongoRepository<User, Integer> {
}
